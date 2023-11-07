// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.BitValue;

/// <summary>
/// Compare64x64UnsignedLessAdd64
/// </summary>
[Transform("IR.Optimizations.Auto.BitValue")]
public sealed class Compare64x64UnsignedLessAdd64 : BaseTransform
{
	public Compare64x64UnsignedLessAdd64() : base(IRInstruction.Compare64x64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, Transform transform)
	{
		if (context.ConditionCode != ConditionCode.UnsignedLess)
			return false;

		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IRInstruction.Add64)
			return false;

		if (!IsResolvedConstant(context.Operand1.Definitions[0].Operand2))
			return false;

		if (!IsResolvedConstant(context.Operand2))
			return false;

		if (IsAddOverflow64(BitValueMax64(context.Operand1.Definitions[0].Operand1), BitValueMax64(context.Operand1.Definitions[0].Operand2)))
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand1;
		var t2 = context.Operand1.Definitions[0].Operand2;
		var t3 = context.Operand2;

		var v1 = transform.VirtualRegisters.Allocate64();

		context.SetInstruction(IRInstruction.Sub64, v1, t3, t2);
		context.AppendInstruction(IRInstruction.Compare64x64, ConditionCode.UnsignedLess, result, t1, v1);
	}
}

/// <summary>
/// Compare64x64UnsignedLessAdd64_v1
/// </summary>
[Transform("IR.Optimizations.Auto.BitValue")]
public sealed class Compare64x64UnsignedLessAdd64_v1 : BaseTransform
{
	public Compare64x64UnsignedLessAdd64_v1() : base(IRInstruction.Compare64x64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, Transform transform)
	{
		if (context.ConditionCode != ConditionCode.UnsignedGreater)
			return false;

		if (!context.Operand2.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsDefinedOnce)
			return false;

		if (context.Operand2.Definitions[0].Instruction != IRInstruction.Add64)
			return false;

		if (!IsResolvedConstant(context.Operand2.Definitions[0].Operand2))
			return false;

		if (!IsResolvedConstant(context.Operand1))
			return false;

		if (IsAddOverflow64(BitValueMax64(context.Operand2.Definitions[0].Operand1), BitValueMax64(context.Operand2.Definitions[0].Operand2)))
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;
		var t2 = context.Operand2.Definitions[0].Operand1;
		var t3 = context.Operand2.Definitions[0].Operand2;

		var v1 = transform.VirtualRegisters.Allocate64();

		context.SetInstruction(IRInstruction.Sub64, v1, t1, t3);
		context.AppendInstruction(IRInstruction.Compare64x64, ConditionCode.UnsignedLess, result, t2, v1);
	}
}
