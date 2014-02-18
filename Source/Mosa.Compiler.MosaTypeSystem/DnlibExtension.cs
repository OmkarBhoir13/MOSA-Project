﻿/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using dnlib.DotNet;

namespace Mosa.Compiler.MosaTypeSystem
{
	static class DnlibExtension
	{
		public static ITypeDefOrRef GetElementType(this TypeSig signature)
		{
			return signature.GetNonNestedTypeRefScope();
		}

		public static TypeSig GetElementSig(this TypeSig signature)
		{
			while (signature.Next != null)
			{
				signature = signature.Next;
				TypeSpec spec = signature.TryGetTypeSpec();
				if (spec != null)
					signature = spec.TypeSig;
			}

			return signature;
		}

		public static MethodDef ResolveMethod(this IMethodDefOrRef method)
		{
			MethodDef result = method as MethodDef;
			if (result != null)
				return result;

			return ((MemberRef)method).ResolveMethodThrow();
		}

		public static bool HasOpenGenericParameter(this TypeSig signature)
		{
			if (signature.IsGenericParameter)
				return true;

			if (signature is ModifierSig)
			{
				TypeSpec modifier = ((ModifierSig)signature).Modifier as TypeSpec;
				if (modifier != null && HasOpenGenericParameter(modifier.TypeSig))
					return true;
			}

			if(signature is NonLeafSig){
				return HasOpenGenericParameter(signature.Next);
			}
			else if (signature is TypeDefOrRefSig)
			{
				TypeSpec type = ((TypeDefOrRefSig)signature).TypeDefOrRef as TypeSpec;
				if (type != null && HasOpenGenericParameter(type.TypeSig))
					return true;
				else 
					return ((TypeDefOrRefSig)signature).TypeDefOrRef.ResolveTypeDef().HasGenericParameters;
			}
			else if (signature is GenericInstSig)
			{
				GenericInstSig genericInst = (GenericInstSig)signature;
				foreach (var genericArg in genericInst.GenericArguments)
				{
					if (HasOpenGenericParameter(genericArg))
						return true;
				}
				TypeSpec genericType = genericInst.GenericType.TypeDefOrRef as TypeSpec;
				if (genericType != null && HasOpenGenericParameter(genericType.TypeSig))
					return true;
			}

			return false;
		}

		public static bool HasOpenGenericParameter(this MethodSig signature)
		{
			if (signature.GenParamCount > 0)
				return true;

			foreach (var param in signature.Params)
			{
				if (HasOpenGenericParameter(param))
					return true;
			}
			return HasOpenGenericParameter(signature.RetType);
		}
	}
}
