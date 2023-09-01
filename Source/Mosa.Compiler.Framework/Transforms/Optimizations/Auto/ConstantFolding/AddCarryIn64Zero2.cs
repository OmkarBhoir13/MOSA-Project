// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.ConstantFolding;

/// <summary>
/// AddCarryIn64Zero2
/// </summary>
[Transform("IR.Optimizations.Auto.ConstantFolding")]
public sealed class AddCarryIn64Zero2 : BaseTransform
{
	public AddCarryIn64Zero2() : base(IRInstruction.AddCarryIn64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsResolvedConstant)
			return false;

		if (context.Operand2.ConstantUnsigned64 != 0)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;
		var t2 = context.Operand3;

		context.SetInstruction(IRInstruction.Add64, result, t1, t2);
	}
}
