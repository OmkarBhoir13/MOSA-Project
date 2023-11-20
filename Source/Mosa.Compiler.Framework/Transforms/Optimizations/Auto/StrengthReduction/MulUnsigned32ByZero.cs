// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.StrengthReduction;

public sealed class MulUnsigned32ByZero : BaseTransform
{
	public MulUnsigned32ByZero() : base(IR.MulUnsigned32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 80;

	public override bool Match(Context context, Transform transform)
	{
		if (!context.Operand2.IsConstantZero)
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var e1 = Operand.Constant32_0;

		context.SetInstruction(IR.Move32, result, e1);
	}
}

public sealed class MulUnsigned32ByZero_v1 : BaseTransform
{
	public MulUnsigned32ByZero_v1() : base(IR.MulUnsigned32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 80;

	public override bool Match(Context context, Transform transform)
	{
		if (!context.Operand1.IsConstantZero)
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var e1 = Operand.Constant32_0;

		context.SetInstruction(IR.Move32, result, e1);
	}
}
