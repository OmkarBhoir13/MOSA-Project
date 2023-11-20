// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Diagnostics;
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.Tweak;

/// <summary>
/// MovLoad16
/// </summary>
public sealed class MovLoad16 : BaseTransform
{
	public MovLoad16() : base(X86.MovLoad16, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, Transform transform)
	{
		if (!context.Result.IsPhysicalRegister)
			return false;

		return context.Result.Register == CPURegister.ESI || context.Result.Register == CPURegister.EDI;
	}

	public override void Transform(Context context, Transform transform)
	{
		Debug.Assert(context.Result.IsPhysicalRegister);

		var result = context.Result;
		var source = context.Operand1;
		var offset = context.Operand2;

		context.SetInstruction(X86.MovLoad32, result, source, offset);
		context.AppendInstruction(X86.And32, result, result, Operand.CreateConstant32(0x0000FFFF));
	}
}
