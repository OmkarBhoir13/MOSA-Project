// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification;

/// <summary>
/// Compare32x64SwapToZero
/// </summary>
[Transform("IR.Optimizations.Auto.Simplification")]
public sealed class Compare32x64SwapToZero : BaseTransform
{
	public Compare32x64SwapToZero() : base(IRInstruction.Compare32x64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (context.ConditionCode != ConditionCode.NotEqual)
			return false;

		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsResolvedConstant)
			return false;

		if (context.Operand2.ConstantUnsigned64 != 1)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IRInstruction.And64)
			return false;

		if (!context.Operand1.Definitions[0].Operand2.IsResolvedConstant)
			return false;

		if (context.Operand1.Definitions[0].Operand2.ConstantUnsigned64 != 1)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand1;

		var v1 = transform.VirtualRegisters.Allocate64();

		var c1 = Operand.CreateConstant(0);
		var c2 = Operand.CreateConstant(1);

		context.SetInstruction(IRInstruction.And64, v1, t1, c2);
		context.AppendInstruction(IRInstruction.Compare32x64, ConditionCode.Equal, result, v1, c1);
	}
}

/// <summary>
/// Compare32x64SwapToZero_v1
/// </summary>
[Transform("IR.Optimizations.Auto.Simplification")]
public sealed class Compare32x64SwapToZero_v1 : BaseTransform
{
	public Compare32x64SwapToZero_v1() : base(IRInstruction.Compare32x64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (context.ConditionCode != ConditionCode.NotEqual)
			return false;

		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsResolvedConstant)
			return false;

		if (context.Operand2.ConstantUnsigned64 != 1)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IRInstruction.And64)
			return false;

		if (!context.Operand1.Definitions[0].Operand1.IsResolvedConstant)
			return false;

		if (context.Operand1.Definitions[0].Operand1.ConstantUnsigned64 != 1)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand2;

		var v1 = transform.VirtualRegisters.Allocate64();

		var c1 = Operand.CreateConstant(0);
		var c2 = Operand.CreateConstant(1);

		context.SetInstruction(IRInstruction.And64, v1, t1, c2);
		context.AppendInstruction(IRInstruction.Compare32x64, ConditionCode.Equal, result, v1, c1);
	}
}
