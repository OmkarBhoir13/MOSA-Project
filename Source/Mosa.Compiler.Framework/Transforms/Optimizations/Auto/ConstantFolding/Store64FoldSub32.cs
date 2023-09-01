// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.ConstantFolding;

/// <summary>
/// Store64FoldSub32
/// </summary>
[Transform("IR.Optimizations.Auto.ConstantFolding")]
public sealed class Store64FoldSub32 : BaseTransform
{
	public Store64FoldSub32() : base(IRInstruction.Store64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IRInstruction.Sub32)
			return false;

		if (!IsResolvedConstant(context.Operand2))
			return false;

		if (!IsResolvedConstant(context.Operand1.Definitions[0].Operand2))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand1;
		var t2 = context.Operand1.Definitions[0].Operand2;
		var t3 = context.Operand2;
		var t4 = context.Operand3;

		var e1 = Operand.CreateConstant(Sub32(To32(t3), To32(t2)));

		context.SetInstruction(IRInstruction.Store64, result, t1, e1, t4);
	}
}
