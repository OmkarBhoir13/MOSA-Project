﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Mosa.Runtime.Metadata;
using Mosa.Runtime.CompilerFramework;
using IR = Mosa.Runtime.CompilerFramework.IR;

namespace Mosa.Platforms.x86
{
	/// <summary>
	/// This is an x86 specific compiler stage to remove floating point constants from instructions.
	/// </summary>
	/// <remarks>
	/// The code generated by the x86 compiler is position independent. However x86 does not allow
	/// embedding floating point values as immediates inside the code, so these have to be moved outside
	/// and referenced through a memory offset starting at the 
	/// </remarks>
	public sealed class ConstantRemovalStage : BaseStage, IMethodCompilerStage, IPipelineStage
	{
		#region Data members

		// Check the type of the constant operand against this list of large CIL types,
		// that need special handling.
		private static CilElementType[] _largeCilTypes = {
                CilElementType.R4,
                CilElementType.R8,
                //CilElementType.I8,
                //CilElementType.U8
            };

		/// <summary>
		/// If true it indicates that this compiler stage has moved at least 
		/// one floating point constant to physical memory.
		/// </summary>
		private bool _constantRemoved;

		#endregion // Data members

		#region Properties

		/// <summary>
		/// Gets the existance of FP constants in the instruction stream.
		/// </summary>
		public bool ConstantRemoved
		{
			get { return _constantRemoved; }
		}

		#endregion // Properties

		#region IPipelineStage Members

		/// <summary>
		/// Retrieves the name of the compilation stage.
		/// </summary>
		/// <value>The name of the compilation stage.</value>
		string IPipelineStage.Name { get { return @"ConstantRemovalStage"; } }

		/// <summary>
		/// Gets the pipeline stage order.
		/// </summary>
		/// <value>The pipeline stage order.</value>
		PipelineStageOrder[] IPipelineStage.PipelineStageOrder
		{
			get
			{
				return new PipelineStageOrder[] {
					new PipelineStageOrder(PipelineStageOrder.Location.After, typeof(EnterSSA)),
					//new PipelineStageOrder(PipelineStageOrder.Location.Before, typeof(IR.CILTransformationStage))
				};
			}
		}

		/// <summary>
		/// Runs the specified method compiler.
		/// </summary>
		/// <param name="compiler">The compiler context to perform processing in.</param>
		public override void Run(IMethodCompiler compiler)
		{
			base.Run(compiler);

			IArchitecture arch = compiler.Architecture;

			Context ctxEpilogue = new Context(InstructionSet, FindBlock(Int32.MaxValue));
			ctxEpilogue.GotoLast();

			// Iterate all blocks and collect locals from all blocks
			foreach (BasicBlock block in BasicBlocks)
				ProcessInstructions(arch, new Context(InstructionSet, block), ctxEpilogue);
		}

		#endregion // IMethodCompilerStage Members

		#region Internals

		/// <summary>
		/// Enumerates all instructions and eliminates floating point constants from them.
		/// </summary>
		/// <param name="architecture">The architecture used to create literals.</param>
		/// <param name="ctx">The context.</param>
		/// <param name="ctxEpilogue">The context of the epilogue.</param>
		private void ProcessInstructions(IArchitecture architecture, Context ctx, Context ctxEpilogue)
		{
			// Current constant operand
			ConstantOperand co = null;

			for (; !ctx.EndOfInstruction; ctx.GotoNext()) {
				// A constant may only appear on the right side of an expression, so we ignore constants in
				// Result - there should never be one there.
				foreach (Operand op in ctx.Operands) {
					co = op as ConstantOperand;
					if (co != null && IsLargeConstant(co)) {

						// Move the constant out of the code stream and place it right after the code.
						ctxEpilogue.AppendInstruction(CPUx86.Instruction.LiteralInstruction);
						ctxEpilogue.LiteralData = new IR.LiteralData(ctx.Label, co.Type, co.Value);

						// FIXME - I hope I got this right
						//co.Replace(literal.CreateOperand());
						op.Replace(((ctxEpilogue.Instruction) as CPUx86.LiteralInstruction).CreateOperand(ctxEpilogue), InstructionSet);

						_constantRemoved = true;
					}

				}
			}
		}

		/// <summary>
		/// Determines if the given constant operand is a large constant.
		/// </summary>
		/// <remarks>
		/// Only constants, which have special types or do not fit into an immediate argument
		/// are large and are moved to memory.
		/// </remarks>
		/// <param name="co">The constant operand to check.</param>
		/// <returns>True if the constant is large and needs to be moved to a literal.</returns>
		private static bool IsLargeConstant(ConstantOperand co)
		{
			return (Array.IndexOf<CilElementType>(_largeCilTypes, co.Type.Type) != -1);
		}

		#endregion // Internals
	}
}
