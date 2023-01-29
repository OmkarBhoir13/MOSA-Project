﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Manual.LowerTo32;

public sealed class LoadParamSignExtend32x64 : BaseTransform
{
	public LoadParamSignExtend32x64() : base(IRInstruction.LoadParamSignExtend32x64, TransformType.Manual | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		return transform.LowerTo32;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;
		var operand1 = context.Operand1;

		transform.SplitLongOperand(operand1, out Operand op0Low, out Operand _);

		var resultLow = transform.AllocateVirtualRegister32();
		var resultHigh = transform.AllocateVirtualRegister32();

		context.SetInstruction(IRInstruction.LoadParam32, resultLow, op0Low);
		context.AppendInstruction(IRInstruction.ArithShiftRight32, resultHigh, resultLow, transform.CreateConstant(31));
		context.AppendInstruction(IRInstruction.To64, result, resultLow, resultHigh);
	}
}
