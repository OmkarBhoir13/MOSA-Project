﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

namespace Mosa.Compiler.Framework.IR
{
	/// <summary>
	///
	/// </summary>
	public static class IRInstruction
	{
		/// <summary>
		///
		/// </summary>
		public static readonly AddFloat AddFloat = new AddFloat();

		/// <summary>
		///
		/// </summary>
		public static readonly AddSigned AddSigned = new AddSigned();

		/// <summary>
		///
		/// </summary>
		public static readonly AddUnsigned AddUnsigned = new AddUnsigned();

		/// <summary>
		///
		/// </summary>
		public static readonly AddressOf AddressOf = new AddressOf();

		/// <summary>
		///
		/// </summary>
		public static readonly ArithmeticShiftRight ArithmeticShiftRight = new ArithmeticShiftRight();

		/// <summary>
		///
		/// </summary>
		public static readonly Break Break = new Break();

		/// <summary>
		///
		/// </summary>
		public static readonly DivFloat DivFloat = new DivFloat();

		/// <summary>
		///
		/// </summary>
		public static readonly DivSigned DivSigned = new DivSigned();

		/// <summary>
		///
		/// </summary>
		public static readonly DivUnsigned DivUnsigned = new DivUnsigned();

		/// <summary>
		///
		/// </summary>
		public static readonly Epilogue Epilogue = new Epilogue();

		/// <summary>
		///
		/// </summary>
		public static readonly FloatCompare FloatCompare = new FloatCompare();

		/// <summary>
		///
		/// </summary>
		public static readonly FloatToIntegerConversion FloatToIntegerConversion = new FloatToIntegerConversion();

		/// <summary>
		///
		/// </summary>
		public static readonly IntegerCompareBranch IntegerCompareBranch = new IntegerCompareBranch();

		/// <summary>
		///
		/// </summary>
		public static readonly IntegerCompare IntegerCompare = new IntegerCompare();

		/// <summary>
		///
		/// </summary>
		public static readonly IntegerToFloatConversion IntegerToFloatConversion = new IntegerToFloatConversion();

		/// <summary>
		///
		/// </summary>
		public static readonly Jmp Jmp = new Jmp();

		/// <summary>
		///
		/// </summary>
		public static readonly Load Load = new Load();

		/// <summary>
		///
		/// </summary>
		public static readonly CompoundLoad CompoundLoad = new CompoundLoad();

		/// <summary>
		///
		/// </summary>
		public static readonly LoadZeroExtended LoadZeroExtended = new LoadZeroExtended();

		/// <summary>
		///
		/// </summary>
		public static readonly LoadSignExtended LoadSignExtended = new LoadSignExtended();

		/// <summary>
		///
		/// </summary>
		public static readonly LogicalAnd LogicalAnd = new LogicalAnd();

		/// <summary>
		///
		/// </summary>
		public static readonly LogicalNot LogicalNot = new LogicalNot();

		/// <summary>
		///
		/// </summary>
		public static readonly LogicalOr LogicalOr = new LogicalOr();

		/// <summary>
		///
		/// </summary>
		public static readonly LogicalXor LogicalXor = new LogicalXor();

		/// <summary>
		///
		/// </summary>
		public static readonly Move Move = new Move();

		/// <summary>
		///
		/// </summary>
		public static readonly CompoundMove CompoundMove = new CompoundMove();

		/// <summary>
		///
		/// </summary>
		public static readonly MulFloat MulFloat = new MulFloat();

		/// <summary>
		///
		/// </summary>
		public static readonly MulSigned MulSigned = new MulSigned();

		/// <summary>
		///
		/// </summary>
		public static readonly MulUnsigned MulUnsigned = new MulUnsigned();

		/// <summary>
		///
		/// </summary>
		public static readonly Phi Phi = new Phi();

		/// <summary>
		///
		/// </summary>
		public static readonly Prologue Prologue = new Prologue();

		/// <summary>
		///
		/// </summary>
		public static readonly RemFloat RemFloat = new RemFloat();

		/// <summary>
		///
		/// </summary>
		public static readonly RemSigned RemSigned = new RemSigned();

		/// <summary>
		///
		/// </summary>
		public static readonly RemUnsigned RemUnsigned = new RemUnsigned();

		/// <summary>
		///
		/// </summary>
		public static readonly Return Return = new Return();

		/// <summary>
		///
		/// </summary>
		public static readonly InternalReturn InternalReturn = new InternalReturn();

		/// <summary>
		///
		/// </summary>
		public static readonly ShiftRight ShiftRight = new ShiftRight();

		/// <summary>
		///
		/// </summary>
		public static readonly ShiftLeft ShiftLeft = new ShiftLeft();

		/// <summary>
		///
		/// </summary>
		public static readonly SignExtendedMove SignExtendedMove = new SignExtendedMove();

		/// <summary>
		///
		/// </summary>
		public static readonly Store Store = new Store();

		/// <summary>
		///
		/// </summary>
		public static readonly CompoundStore CompoundStore = new CompoundStore();

		/// <summary>
		///
		/// </summary>
		public static readonly SubFloat SubFloat = new SubFloat();

		/// <summary>
		///
		/// </summary>
		public static readonly SubSigned SubSigned = new SubSigned();

		/// <summary>
		///
		/// </summary>
		public static readonly SubUnsigned SubUnsigned = new SubUnsigned();

		/// <summary>
		///
		/// </summary>
		public static readonly ZeroExtendedMove ZeroExtendedMove = new ZeroExtendedMove();

		/// <summary>
		///
		/// </summary>
		public static readonly Call Call = new Call();

		/// <summary>
		///
		/// </summary>
		public static readonly Nop Nop = new Nop();

		/// <summary>
		///
		/// </summary>
		public static readonly Switch Switch = new Switch();

		/// <summary>
		///
		/// </summary>
		public static readonly Throw Throw = new Throw();

		/// <summary>
		///
		/// </summary>
		public static readonly ExceptionPrologue ExceptionPrologue = new ExceptionPrologue();

		/// <summary>
		///
		/// </summary>
		public static readonly IntrinsicMethodCall IntrinsicMethodCall = new IntrinsicMethodCall();

		/// <summary>
		///
		/// </summary>
		public static readonly BlockEnd BlockEnd = new BlockEnd();

		/// <summary>
		///
		/// </summary>
		public static readonly BlockStart BlockStart = new BlockStart();
	}
}