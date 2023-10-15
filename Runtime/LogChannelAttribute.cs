namespace Guyl.Logger
{
	using System;

	[AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
	public class LogChannelAttribute : Attribute { }
}