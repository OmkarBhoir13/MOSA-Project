// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.IR;

/// <summary>
/// PhiManagedPointer
/// </summary>
/// <seealso cref="Mosa.Compiler.Framework.IR.BaseIRInstruction" />
public sealed class PhiManagedPointer : BaseIRInstruction
{
	public PhiManagedPointer()
		: base(0, 0)
	{
	}

	public override bool IsPhiInstruction => true;

	public override bool VariableOperands => true;
}
