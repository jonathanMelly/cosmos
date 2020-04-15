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
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="CosmosParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface ICosmosVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.programme"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgramme([NotNull] CosmosParser.ProgrammeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.entete"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEntete([NotNull] CosmosParser.EnteteContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.auteur"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAuteur([NotNull] CosmosParser.AuteurContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.date"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDate([NotNull] CosmosParser.DateContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.entreprise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEntreprise([NotNull] CosmosParser.EntrepriseContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.description"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDescription([NotNull] CosmosParser.DescriptionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.mainStart"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMainStart([NotNull] CosmosParser.MainStartContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.mainEnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMainEnd([NotNull] CosmosParser.MainEndContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.instruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInstruction([NotNull] CosmosParser.InstructionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.instruction_simple"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInstruction_simple([NotNull] CosmosParser.Instruction_simpleContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.instruction_complexe"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInstruction_complexe([NotNull] CosmosParser.Instruction_complexeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.afficher"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAfficher([NotNull] CosmosParser.AfficherContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.allouer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAllouer([NotNull] CosmosParser.AllouerContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.affecter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAffecter([NotNull] CosmosParser.AffecterContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.zone_memoire"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitZone_memoire([NotNull] CosmosParser.Zone_memoireContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.selection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSelection([NotNull] CosmosParser.SelectionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.sinon_si"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSinon_si([NotNull] CosmosParser.Sinon_siContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.sinon"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSinon([NotNull] CosmosParser.SinonContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondition([NotNull] CosmosParser.ConditionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.postcondition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPostcondition([NotNull] CosmosParser.PostconditionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.operateur_comparaison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperateur_comparaison([NotNull] CosmosParser.Operateur_comparaisonContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.operateur_booleen"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperateur_booleen([NotNull] CosmosParser.Operateur_booleenContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] CosmosParser.ExpressionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.expression_calculable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression_calculable([NotNull] CosmosParser.Expression_calculableContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.expression_representant_numeraire"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression_representant_numeraire([NotNull] CosmosParser.Expression_representant_numeraireContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.expression_variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression_variable([NotNull] CosmosParser.Expression_variableContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.expression_textuelle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression_textuelle([NotNull] CosmosParser.Expression_textuelleContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.expression_numeraire"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression_numeraire([NotNull] CosmosParser.Expression_numeraireContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="CosmosParser.operateur_mathematique"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperateur_mathematique([NotNull] CosmosParser.Operateur_mathematiqueContext context);
}
} // namespace interpreter.antlr
