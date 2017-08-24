using System;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace App.Client.Web.Infrastructure
{
	public class ControllerConvention : IRegistrationConvention
	{
		public void ScanTypes(TypeSet types, Registry registry)
		{
			foreach (Type type in types.AllTypes())
			{
				if (type.CanBeCastTo(typeof (Controller)) && !type.IsAbstract)
				{
					registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
				}
			}
		}
	}
}