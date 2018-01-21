// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.IR
{
	/// <summary>
	/// Gen
	/// </summary>
	/// <seealso cref="Mosa.Compiler.Framework.IR.BaseIRInstruction" />
	public sealed class Gen : BaseIRInstruction
	{
		public Gen()
			: base(0, 1)
		{
		}

		public override bool IgnoreDuringCodeGeneration { get { return true; } }

		public override bool VariableOperands { get { return true; } }
	}
}

