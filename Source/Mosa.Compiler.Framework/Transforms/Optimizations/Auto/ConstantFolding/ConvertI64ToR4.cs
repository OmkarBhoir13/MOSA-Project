// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.ConstantFolding;

public sealed class ConvertI64ToR4 : BaseTransform
{
	public ConvertI64ToR4() : base(IR.ConvertI64ToR4, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 100;

	public override bool Match(Context context, Transform transform)
	{
		if (!IsResolvedConstant(context.Operand1))
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;

		var e1 = Operand.CreateConstant(ToR4(ToSigned64(t1)));

		context.SetInstruction(IR.MoveR4, result, e1);
	}
}
