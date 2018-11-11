// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x86.Instructions
{
	/// <summary>
	/// Movd
	/// </summary>
	/// <seealso cref="Mosa.Platform.x86.X86Instruction" />
	public sealed class Movd : X86Instruction
	{
		public override int ID { get { return 258; } }

		internal Movd()
			: base(1, 1)
		{
		}

		public override void Emit(InstructionNode node, BaseCodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == 1);
			System.Diagnostics.Debug.Assert(node.OperandCount == 1);

			emitter.OpcodeEncoder.AppendNibble(0b0110);
			emitter.OpcodeEncoder.AppendNibble(0b0110);
			emitter.OpcodeEncoder.AppendNibble(0b0000);
			emitter.OpcodeEncoder.AppendNibble(0b1111);
			emitter.OpcodeEncoder.AppendNibble(0b0111);
			emitter.OpcodeEncoder.AppendNibble(0b1110);
			emitter.OpcodeEncoder.Append2Bits(0b11);
			emitter.OpcodeEncoder.Append3Bits(node.Operand1.Register.RegisterCode);
			emitter.OpcodeEncoder.Append3Bits(node.Result.Register.RegisterCode);
		}
	}
}
