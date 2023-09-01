﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x86.Intrinsic::Sqrtsd")]
	private static void Sqrtsd(Context context, TransformContext transformContext)
	{
		context.SetInstruction(X86.Sqrtsd, context.Result, context.Operand1);
	}
}
