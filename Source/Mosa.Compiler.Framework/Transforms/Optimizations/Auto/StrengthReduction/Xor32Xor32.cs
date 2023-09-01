// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.StrengthReduction;

/// <summary>
/// Xor32Xor32
/// </summary>
[Transform("IR.Optimizations.Auto.StrengthReduction")]
public sealed class Xor32Xor32 : BaseTransform
{
	public Xor32Xor32() : base(IRInstruction.Xor32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 80;

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsDefinedOnce)
			return false;

		if (context.Operand2.Definitions[0].Instruction != IRInstruction.Xor32)
			return false;

		if (!AreSame(context.Operand1, context.Operand2.Definitions[0].Operand1))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand2.Definitions[0].Operand2;

		context.SetInstruction(IRInstruction.Move32, result, t1);
	}
}

/// <summary>
/// Xor32Xor32_v1
/// </summary>
[Transform("IR.Optimizations.Auto.StrengthReduction")]
public sealed class Xor32Xor32_v1 : BaseTransform
{
	public Xor32Xor32_v1() : base(IRInstruction.Xor32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 80;

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IRInstruction.Xor32)
			return false;

		if (!AreSame(context.Operand1.Definitions[0].Operand1, context.Operand2))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand2;

		context.SetInstruction(IRInstruction.Move32, result, t1);
	}
}

/// <summary>
/// Xor32Xor32_v2
/// </summary>
[Transform("IR.Optimizations.Auto.StrengthReduction")]
public sealed class Xor32Xor32_v2 : BaseTransform
{
	public Xor32Xor32_v2() : base(IRInstruction.Xor32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 80;

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsDefinedOnce)
			return false;

		if (context.Operand2.Definitions[0].Instruction != IRInstruction.Xor32)
			return false;

		if (!AreSame(context.Operand1, context.Operand2.Definitions[0].Operand2))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand2.Definitions[0].Operand1;

		context.SetInstruction(IRInstruction.Move32, result, t1);
	}
}

/// <summary>
/// Xor32Xor32_v3
/// </summary>
[Transform("IR.Optimizations.Auto.StrengthReduction")]
public sealed class Xor32Xor32_v3 : BaseTransform
{
	public Xor32Xor32_v3() : base(IRInstruction.Xor32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 80;

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IRInstruction.Xor32)
			return false;

		if (!AreSame(context.Operand1.Definitions[0].Operand2, context.Operand2))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand1;

		context.SetInstruction(IRInstruction.Move32, result, t1);
	}
}
