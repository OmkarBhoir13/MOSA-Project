﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Reflection.Metadata.Ecma335;

namespace Mosa.Compiler.Framework.Stages;

/// <summary>
///	Base Transform Stage
/// </summary>
public abstract class BaseTransformStage : BaseMethodCompilerStage
{
	private const int MaximumInstructionID = 1000;
	private const int MaximumPasses = 20;

	private int TransformCount;
	private int OptimizationCount;

	private readonly List<BaseTransform>[] transforms = new List<BaseTransform>[MaximumInstructionID];
	private readonly List<BaseBlockTransform> blockTransforms = new();

	protected TraceLog Trace;

	protected TraceLog SpecialTrace;

	protected bool EnableTransformOptimizations;
	protected bool EnableBlockOptimizations;
	protected bool AreCPURegistersAllocated;

	protected int MaxPasses;

	protected readonly Dictionary<string, Counter> TransformCounters = new();

	private bool SortedByPriority;

	private string TransformCountStage;
	private string OptimizationCountStage;

	public BaseTransformStage(int maxPasses = MaximumPasses)
	{
		MaxPasses = maxPasses;
		EnableTransformOptimizations = false;
		EnableBlockOptimizations = false;
	}

	protected override void Initialize()
	{
		TransformCountStage = $"{Name}.Transforms";
		OptimizationCountStage = $"{Name}.Optimizations";
	}

	protected override void Finish()
	{
		UpdateCounter("Transform.Count", Transform.TransformCount);
		UpdateCounter("Transform.Count.Transforms", TransformCount);
		UpdateCounter("Transform.Count.Optimizations", OptimizationCount);

		UpdateCounter(TransformCountStage, TransformCount);
		UpdateCounter(OptimizationCountStage, OptimizationCount);

		MethodCompiler.Compiler.PostTraceLog(SpecialTrace);

		TransformCount = 0;
		OptimizationCount = 0;

		Trace = null;
	}

	protected override void Run()
	{
		SortByPriority();

		Trace = CreateTraceLog(5);

		AreCPURegistersAllocated = MethodCompiler.AreCPURegistersAllocated;

		SpecialTrace = new TraceLog(TraceType.GlobalDebug, null, null, "Special Optimizations");

		Transform.SetLogs(Trace, SpecialTrace);

		Transform.TraceInstructions();

		Setup();

		ExecutePhases();
	}

	protected void AddTranforms(List<BaseTransform> list)
	{
		foreach (var transform in list)
		{
			AddTranform(transform);
		}
	}

	protected void AddTranform(BaseTransform transform)
	{
		var id = transform.Instruction == null ? 0 : transform.Instruction.ID;

		if (transforms[id] == null)
		{
			transforms[id] = new List<BaseTransform>();
		}

		transforms[id].Add(transform);

		EnableTransformOptimizations = true;
	}

	protected void AddTranforms(List<BaseBlockTransform> list)
	{
		foreach (var transform in list)
		{
			blockTransforms.Add(transform);
		}
	}

	private void SortByPriority()
	{
		if (SortedByPriority)
			return;

		foreach (var list in transforms)
		{
			if (list == null)
				continue;

			list.Sort();
			list.Reverse();
		}

		SortedByPriority = true;
	}

	protected virtual void Setup()
	{
	}

	protected virtual bool SetupPhase(int phase)
	{
		return phase == 0;
	}

	private void ExecutePhases()
	{
		var phase = 0;
		var pass = 1;

		while (true)
		{
			if (!SetupPhase(phase++))
				break;

			pass = ExecutePasses(pass, MaxPasses);
		}
	}

	private int ExecutePasses(int pass, int maxPasses)
	{
		var changed = true;
		var firstPass = true;

		while (changed)
		{
			Trace?.Log($"*** Pass # {pass}");

			var changed1 = InstructionTransformationPass();
			var changed2 = (!changed1 || firstPass) && ApplyBlockTransforms();

			changed = changed1 || changed2;

			pass++;
			firstPass = false;

			if (pass >= maxPasses && maxPasses != 0)
				break;
		}

		return pass;
	}

	private bool InstructionTransformationPass()
	{
		if (!EnableTransformOptimizations)
			return false;

		var context = new Context(BasicBlocks.PrologueBlock);

		var changed = false;

		for (var i = 0; i < BasicBlocks.Count; i++)
		{
			for (var node = BasicBlocks[i].AfterFirst; !node.IsBlockEndInstruction; node = node.Next)
			{
				context.Node = node;

				var updated = Process(context);
				changed = changed || updated;
			}
		}

		return changed;
	}

	private bool Process(Context context)
	{
		var updated = true;
		var changed = false;

		while (updated)
		{
			if (context.IsEmptyOrNop)
				break;

			updated = ApplyTransformations(context);

			changed |= updated;
		}

		return changed;
	}

	private bool ApplyTransformations(Context context)
	{
		if (ApplyTransformations(context, 0))
			return true;

		return ApplyTransformations(context, context.Instruction.ID);
	}

	private bool ApplyTransformations(Context context, int id)
	{
		var instructionTransforms = transforms[id];

		if (instructionTransforms == null)
			return false;

		var count = instructionTransforms.Count;

		for (var i = 0; i < count; i++)
		{
			var transform = instructionTransforms[i];

			var updated = Transform.ApplyTransform(context, transform);

			if (updated)
			{
				if (transform.IsOptimization)
					OptimizationCount++;
				else if (transform.IsTranformation)
					TransformCount++;

				if (MethodCompiler.Statistics)
					UpdateCounter(transform.Name, 1);

				if (MosaSettings.FullCheckMode)
					FullCheck(false);

				return true;
			}
		}

		return false;
	}

	private bool ApplyBlockTransforms()
	{
		var changed = false;

		foreach (var transform in blockTransforms)
		{
			var count = transform.Process(Transform);

			var updated = count != 0;
			changed |= updated;

			if (updated)
			{
				if (MosaSettings.FullCheckMode)
					FullCheck(false);

				if (MethodCompiler.Statistics)
					UpdateCounter(transform.Name, count);
			}
		}

		return changed;
	}
}
