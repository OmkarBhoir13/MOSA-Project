/*
 * (c) 2011 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using System.IO;

using Mosa.Compiler.Common;
using Mosa.Compiler.Linker;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Framework.Stages
{
	/// <summary>
	/// Emits metadata for assemblies and types
	/// </summary>
	public sealed class MetadataStage : BaseCompilerStage, ICompilerStage
	{
		#region ICompilerStage members

		void ICompilerStage.Setup(BaseCompiler compiler)
		{
			base.Setup(compiler);
		}

		void ICompilerStage.Run()
		{
			CreateTypeDefinitionTables();
		}

		#endregion ICompilerStage members

		private void EmitStringWithLength(EndianAwareBinaryWriter stream, string value)
		{
			stream.Write(value.Length);
			stream.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(value));
		}

		private void CreateTypeDefinitionTables()
		{
			foreach (var type in typeSystem.AllTypes)
			{
				if (type.IsModule)
					continue;

				// For reflection, even types having opening generic params should be written
				//if (type.HasOpenGenericParams)
				//    continue;

				if (type.BaseType == null && !type.IsInterface && type.FullName != "System.Object")   // ghost types like generic params, function ptr, etc.
					continue;

				CreateTypeDefinitionTable(type);
			}
		}

		private void CreateTypeDefinitionTable(MosaType type)
		{
			string typeNameSymbol = type + @"$tname";

			// Emit type name
			using (Stream stream = linker.Allocate(typeNameSymbol, SectionKind.ROData, 0, typeLayout.NativePointerAlignment))
			{
				using (EndianAwareBinaryWriter writer = new EndianAwareBinaryWriter(stream, architecture.Endianness))
				{
					EmitStringWithLength(writer, type.FullName);
				}
			}

			string typeTableSymbol = type.FullName + @"$dtable";

			using (Stream stream = linker.Allocate(typeTableSymbol, SectionKind.ROData, 0, typeLayout.NativePointerAlignment))
			{
				using (EndianAwareBinaryWriter writer = new EndianAwareBinaryWriter(stream, architecture.Endianness))
				{
					// 1. Size
					writer.Write((uint)typeLayout.GetTypeSize(type));

					// 2. Metadata Token
					//writer.Write((uint)type.Token.ToUInt32());
					writer.Write((uint)0); //FIXME! ^^^

					// 3. Pointer to Name
					linker.Link(LinkType.AbsoluteAddress | LinkType.I4, BuiltInPatch.I4, typeTableSymbol, (int)writer.Position, 0, typeNameSymbol, 0);
					writer.Position += typeLayout.NativePointerSize;

					// 4. Pointer to Assembly Definition
					//linker.Link(LinkType.AbsoluteAddress | LinkType.I4, BuiltInPatch.I4, typeTableSymbol, (int)writer.Position, 0, assemblySymbol, 0);
					writer.Position += typeLayout.NativePointerSize;

					// 5. TODO: Constructor that accepts no parameters, if any, for this type
					writer.WriteZeroBytes(typeLayout.NativePointerSize);

					// 6. Flag: IsInterface
					writer.WriteByte((byte)(type.IsInterface ? 1 : 0));
				}
			}

			CreateFieldDefinitions(type);
		}

		private void CreateFieldDefinitions(MosaType type)
		{
			foreach (MosaField field in type.Fields)
			{
				string fieldNameSymbol = field.FullName + @"$name";

				// Emit field name
				using (Stream stream = linker.Allocate(fieldNameSymbol, SectionKind.ROData, 0, typeLayout.NativePointerAlignment))
				{
					using (EndianAwareBinaryWriter writer = new EndianAwareBinaryWriter(stream, architecture.Endianness))
					{
						EmitStringWithLength(writer, field.Name);
					}
				}


				string fieldDescSymbol = field.FullName + @"$desc";

				// Emit field descriptor
				using (Stream stream = linker.Allocate(fieldDescSymbol, SectionKind.ROData, 0, typeLayout.NativePointerAlignment))
				{
					using (EndianAwareBinaryWriter writer = new EndianAwareBinaryWriter(stream, architecture.Endianness))
					{
						// 1. Offset / Address
						if (field.IsStaticField && !field.IsLiteralField)
						{
							linker.Link(LinkType.AbsoluteAddress | LinkType.I4, BuiltInPatch.I4, fieldDescSymbol, (int)writer.Position, 0, field.FullName, 0);
						}
						else
						{
							writer.Write(typeLayout.GetFieldOffset(field));
							writer.Position -= 4;
						}
						writer.Position += typeLayout.NativePointerSize;

						// 2. Name
						linker.Link(LinkType.AbsoluteAddress | LinkType.I4, BuiltInPatch.I4, fieldDescSymbol, (int)writer.Position, 0, fieldNameSymbol, 0);
						writer.Position += typeLayout.NativePointerSize;

						// 3. Size
						writer.Write((uint)typeLayout.GetFieldSize(field));

						// 4. Metadata Token
						writer.Write((uint)0); //FIXME!
					}
				}
			}
		}
	}
}