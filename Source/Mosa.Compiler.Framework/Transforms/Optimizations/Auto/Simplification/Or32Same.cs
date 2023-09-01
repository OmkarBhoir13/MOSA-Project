// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification;

/// <summary>
/// Or32Same
/// </summary>
[Transform("IR.Optimizations.Auto.Simplification")]
public sealed class Or32Same : BaseTransform
{
	public Or32Same() : base(IRInstruction.Or32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!AreSame(context.Operand1, context.Operand2))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;

		context.SetInstruction(IRInstruction.Move32, result, t1);
	}
}
