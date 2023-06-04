﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x64.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Platform.x64.Intrinsic::In32")]
	private static void In32(Context context, TransformContext transformContext)
	{
		var v1 = transformContext.VirtualRegisters.Allocate64();

		var result = context.Result;

		context.SetInstruction(X64.In32, v1, context.Operand1);
		context.AppendInstruction(X64.Movzx16To64, result, v1);
	}
}
