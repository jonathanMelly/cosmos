//langage de type 'pseudo-code' pour apprendre à programmer
grammar Cosmos ;

//Removes clscompliant warning on build
@header {#pragma warning disable 3021}

programme : entete RETCHAR+ (contexte RETCHAR+)? DEBUT RETCHAR+ instruction_isolee+  FIN EOF ;

entete : auteur RETCHAR date RETCHAR entreprise RETCHAR description ;

auteur : LABEL_AUTEUR MOT MOT? ;
LABEL_AUTEUR : 'Auteur:' ;

date : LABEL_DATE CONTENU_DATE ; 
LABEL_DATE : 'Date:' ;
CONTENU_DATE :  CHIFFRE+ POINT CHIFFRE+ (POINT CHIFFRE+)? ;

entreprise : ENTREPRISE_ENTETE MOT ;
ENTREPRISE_ENTETE : 'Entreprise:' ;

description : DESCRIPTION_ENTETE MOT+ (VIRGULE RETCHAR MOT+)* ;
DESCRIPTION_ENTETE : 'Description:' ;

//optionnel
contexte : CONTEXTE RETCHAR+ TITRE ;
TITRE : 'Titre:' ESPACE? MOT ;
CONTEXTE : 'Contexte:' ESPACE? MOT+ ;

DEBUT : 'Voici mes ordres:' ;
FIN   : 'Fin.' ;

instruction_isolee : (instruction_simple_isolee | instruction_complexe_isolee) ;
instruction_integree : (instruction_simple_integree | instruction_complexe_integree) ;

instruction_simple_base   : TAB+ (afficher)  ;
instruction_simple_isolee   : instruction_simple_base POINT RETCHAR ;
instruction_simple_integree   : instruction_simple_base VIRGULE? RETCHAR ;

instruction_complexe_base : TAB+ (selection) ;
instruction_complexe_isolee : instruction_complexe_base TAB+ POINT RETCHAR ;
instruction_complexe_integree : instruction_complexe_base TAB+ VIRGULE? RETCHAR ;

afficher : FONCTION_AFFICHER  expression_valeur;
FONCTION_AFFICHER : 'Afficher';

selection : DEBUT_CONDITION condition SUITE_CONDITION RETCHAR instruction_integree+ ;
DEBUT_CONDITION : 'Si' ;
SUITE_CONDITION : 'alors' ;
ALTERNATIVE_CONDITION : 'sinon' ;

condition : left=expression_valeur operateur_comparaison right=expression_valeur ;
operateur_comparaison : (OPERATEUR_EGAL | OPERATEUR_DIFFERENT) ;
OPERATEUR_EGAL : 'vaut' | 'est égal à' ;
OPERATEUR_DIFFERENT : 'est différent de' ;

expression_valeur : (expression_textuelle | expression_numeraire) ;

expression_textuelle : LE_TEXTE? VALEUR_TEXTE ;
LE_TEXTE : 'le texte' ;
VALEUR_TEXTE : '"' ~["]* '"' ;

expression_numeraire : LE_NOMBRE? VALEUR_NOMBRE ;
LE_NOMBRE : 'le' ESPACE+ 'nombre' ;
VALEUR_NOMBRE : CHIFFRE+ ;

//fragment LIGNE_DE_TEXTE : MOT (' ' MOT)* ;

MOT : LETTRE+ ;

fragment CHIFFRE : '0'..'9' ;

fragment MINUSCULE : 'a'..'z' ;
fragment MAJUSCULE : 'A'..'Z' ;
fragment SYMBOLES_LETTRE : '-' ;
fragment LETTRE : MINUSCULE | MAJUSCULE | SYMBOLES_LETTRE ;

VIRGULE : ',' ;
POINT: '.' ;

TAB : '\t' | '    ' ;
RETCHAR : '\r'? '\n' ;

//fragment CARACTERE : ~[."\\\r\n ] ;

ESPACE: ' '+ -> skip ;
COMMENTAIRE_LIGNE: (TAB? | TAB+) '//' ~[\n]* RETCHAR -> skip ;
COMMENTAIRE : '/*' .*? '*/' -> skip ;
NOOP : TAB+ RETCHAR -> skip ;

//WS: [ \t\r\n\u000C]+ -> channel(HIDDEN);
//WS : [ \r\n\t] + -> skip ;
//WS : [ ] + -> channel(HIDDEN) ;