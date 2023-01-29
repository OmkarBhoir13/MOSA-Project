// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;
using Mosa.Compiler.Framework.Transforms;

namespace Mosa.Platform.ARMv8A32.Transforms.IR;

/// <summary>
/// Sub64
/// </summary>
public sealed class Sub64 : BaseTransform
{
	public Sub64() : base(IRInstruction.Sub64, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		transform.SplitLongOperand(context.Result, out var resultLow, out var resultHigh);
		transform.SplitLongOperand(context.Operand1, out var op1L, out var op1H);
		transform.SplitLongOperand(context.Operand2, out var op2L, out var op2H);

		op1L = ARMv8A32TransformHelper.MoveConstantToRegister(transform, context, op1L);
		op1H = ARMv8A32TransformHelper.MoveConstantToRegister(transform, context, op1H);
		op2L = ARMv8A32TransformHelper.MoveConstantToRegisterOrImmediate(transform, context, op2L);
		op2H = ARMv8A32TransformHelper.MoveConstantToRegisterOrImmediate(transform, context, op2H);

		context.SetInstruction(ARMv8A32.Sub, StatusRegister.Set, resultLow, op1L, op2L);
		context.AppendInstruction(ARMv8A32.Sbc, resultHigh, op1H, op2H);
	}
}
