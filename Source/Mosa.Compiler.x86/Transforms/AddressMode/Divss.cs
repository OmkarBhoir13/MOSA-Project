// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.AddressMode;

/// <summary>
/// Divss
/// </summary>
[Transform("x86.AddressMode")]
public sealed class Divss : BaseAddressModeTransform
{
	public Divss() : base(X86.Divss, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		AddressModeConversion(context, X86.Movss);
	}
}
