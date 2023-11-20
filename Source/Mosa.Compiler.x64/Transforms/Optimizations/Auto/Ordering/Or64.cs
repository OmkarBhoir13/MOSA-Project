// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.Optimizations.Auto.Ordering;

public sealed class Or64 : BaseTransform
{
	public Or64() : base(X64.Or64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 10;

	public override bool Match(Context context, Transform transform)
	{
		if (!IsVirtualRegister(context.Operand1))
			return false;

		if (!IsVirtualRegister(context.Operand2))
			return false;

		if (!IsGreater(UseCount(context.Operand1), UseCount(context.Operand2)))
			return false;

		if (IsResultAndOperand1Same(context))
			return false;

		return true;
	}

	public override void Transform(Context context, Transform transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;
		var t2 = context.Operand2;

		context.SetInstruction(X64.Or64, result, t2, t1);
	}
}
