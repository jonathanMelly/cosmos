﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Volumes/nFo/data/etml/codespace/csharp/cosmos/src/interpreter/Cosmos.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace interpreter.antlr {
#pragma warning disable 3021
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="CosmosParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface ICosmosListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.programme"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgramme([NotNull] CosmosParser.ProgrammeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.programme"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgramme([NotNull] CosmosParser.ProgrammeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.entete"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEntete([NotNull] CosmosParser.EnteteContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.entete"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEntete([NotNull] CosmosParser.EnteteContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.auteur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAuteur([NotNull] CosmosParser.AuteurContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.auteur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAuteur([NotNull] CosmosParser.AuteurContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.date"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDate([NotNull] CosmosParser.DateContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.date"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDate([NotNull] CosmosParser.DateContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.entreprise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEntreprise([NotNull] CosmosParser.EntrepriseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.entreprise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEntreprise([NotNull] CosmosParser.EntrepriseContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.description"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDescription([NotNull] CosmosParser.DescriptionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.description"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDescription([NotNull] CosmosParser.DescriptionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.contexte"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterContexte([NotNull] CosmosParser.ContexteContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.contexte"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitContexte([NotNull] CosmosParser.ContexteContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.instruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInstruction([NotNull] CosmosParser.InstructionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.instruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInstruction([NotNull] CosmosParser.InstructionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CosmosParser.afficher"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAfficher([NotNull] CosmosParser.AfficherContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CosmosParser.afficher"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAfficher([NotNull] CosmosParser.AfficherContext context);
}
} // namespace interpreter.antlr
