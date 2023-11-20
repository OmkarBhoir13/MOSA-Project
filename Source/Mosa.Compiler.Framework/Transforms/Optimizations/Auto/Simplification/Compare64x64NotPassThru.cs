// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification;

public sealed class Compare64x64NotPassThru : BaseTransform
{
	public Compare64x64NotPassThru() : base(IR.Compare64x64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, Transform transform)
	{
		if (context.ConditionCode != ConditionCode.Equal)
			return false;

		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsConstantZero)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IR.And64)
			return false;

		if (!context.Operand1.Definitions[0].Operand2.IsConstantOne)
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand1;

		var v1 = transform.VirtualRegisters.Allocate64();

		var c1 = Operand.CreateConstant(1);

		context.SetInstruction(IR.Not64, v1, t1);
		context.AppendInstruction(IR.And64, result, v1, c1);
	}
}

public sealed class Compare64x64NotPassThru_v1 : BaseTransform
{
	public Compare64x64NotPassThru_v1() : base(IR.Compare64x64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, Transform transform)
	{
		if (context.ConditionCode != ConditionCode.Equal)
			return false;

		if (!context.Operand1.IsConstantZero)
			return false;

		if (!context.Operand2.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsDefinedOnce)
			return false;

		if (context.Operand2.Definitions[0].Instruction != IR.And64)
			return false;

		if (!context.Operand2.Definitions[0].Operand2.IsConstantOne)
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var t1 = context.Operand2.Definitions[0].Operand1;

		var v1 = transform.VirtualRegisters.Allocate64();

		var c1 = Operand.CreateConstant(1);

		context.SetInstruction(IR.Not64, v1, t1);
		context.AppendInstruction(IR.And64, result, v1, c1);
	}
}

public sealed class Compare64x64NotPassThru_v2 : BaseTransform
{
	public Compare64x64NotPassThru_v2() : base(IR.Compare64x64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, Transform transform)
	{
		if (context.ConditionCode != ConditionCode.Equal)
			return false;

		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsConstantZero)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IR.And64)
			return false;

		if (!context.Operand1.Definitions[0].Operand1.IsConstantOne)
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand2;

		var v1 = transform.VirtualRegisters.Allocate64();

		var c1 = Operand.CreateConstant(1);

		context.SetInstruction(IR.Not64, v1, t1);
		context.AppendInstruction(IR.And64, result, v1, c1);
	}
}

public sealed class Compare64x64NotPassThru_v3 : BaseTransform
{
	public Compare64x64NotPassThru_v3() : base(IR.Compare64x64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, Transform transform)
	{
		if (context.ConditionCode != ConditionCode.Equal)
			return false;

		if (!context.Operand1.IsConstantZero)
			return false;

		if (!context.Operand2.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsDefinedOnce)
			return false;

		if (context.Operand2.Definitions[0].Instruction != IR.And64)
			return false;

		if (!context.Operand2.Definitions[0].Operand1.IsConstantOne)
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var t1 = context.Operand2.Definitions[0].Operand2;

		var v1 = transform.VirtualRegisters.Allocate64();

		var c1 = Operand.CreateConstant(1);

		context.SetInstruction(IR.Not64, v1, t1);
		context.AppendInstruction(IR.And64, result, v1, c1);
	}
}
