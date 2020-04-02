//langage de type 'pseudo-code' pour apprendre à programmer
grammar Cosmos ;

//Removes clscompliant warning on build
@header {#pragma warning disable 3021}

programme : entete RETCHAR+ (contexte RETCHAR+)? DEBUT RETCHAR+ instruction+  FIN EOF ;

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

//commentaire : COMMENTAIRE_DEBUT  ;
COMMENTAIRE_LIGNE: (TAB? | TAB+) '//' ~[\r\n]* -> skip ;
COMMENTAIRE : '/*' .*? '*/' -> skip ;

NOOP : TAB+ RETCHAR -> skip ;

instruction : TAB+ (afficher) POINT RETCHAR+ ;

afficher : FONCTION_AFFICHER (LE_TEXTE? VALEUR_TEXTE | LE_NOMBRE? VALEUR_NOMBRE) ;
FONCTION_AFFICHER : 'Afficher';

LE_TEXTE : 'le' ESPACE+ 'texte' ;
LE_NOMBRE : 'le' ESPACE+ 'nombre' ;

VALEUR_TEXTE : '"' MOT '"' ;
VALEUR_NOMBRE : CHIFFRE+ ;

//fragment LIGNE_DE_TEXTE : MOT (' ' MOT)* ;

MOT : LETTRE+ ;

fragment CHIFFRE : '0'..'9' ;

fragment MINUSCULE : 'a'..'z' ;
fragment MAJUSCULE : 'A'..'Z' ;
fragment SYMBOLES_LETTRE : '-' ;
fragment LETTRE : MINUSCULE | MAJUSCULE | SYMBOLES_LETTRE ;

VIRGULE : ',' ;

TAB : '\t' | '    ' ;
RETCHAR : '\r'? '\n' ;

//fragment CARACTERE : ~[."\\\r\n ] ;

POINT: '.' ;
ESPACE: ' '+ -> skip ;

//WS: [ \t\r\n\u000C]+ -> channel(HIDDEN);
//WS : [ \r\n\t] + -> skip ;
//WS : [ ] + -> channel(HIDDEN) ;