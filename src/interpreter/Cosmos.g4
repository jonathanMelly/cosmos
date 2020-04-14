//langage de type 'pseudo-code' pour apprendre à programmer
grammar Cosmos ;

//Removes clscompliant warning on build
@header {#pragma warning disable 3021}

programme : entete RETCHAR+ mainStart RETCHAR+ instruction+ RETCHAR? RETCHAR? RETCHAR? mainEnd .*? EOF ;

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

mainStart: DEBUT nomDuProgramme=MOT (BIBLIOTHEQUE bibliotheque=MOT)? DEUX_POINT ;
mainEnd: FIN DE_LA_TRANSMISSION? POINT;
DEBUT : 'Voici les ordres du programme' ;
BIBLIOTHEQUE: 'à classer dans la bibliothèque' ;
FIN   : 'Fin' ;
DE_LA_TRANSMISSION : 'de la transmission' ;

instruction : (instruction_simple | instruction_complexe) ;

instruction_simple   : TAB+ (afficher|allouer|affecter) POINT RETCHAR ; //terminaison identique pour chaque
instruction_complexe : TAB+ (selection) ; //terminaison spécifique pour chaque

afficher : 'Afficher'  expression_valeur;
allouer : 'Allouer' zone_memoire ('avec' expression_valeur)? ;
affecter : ('Copier' expression_valeur 'dans' zone_memoire) | (expression_variable OPERATEUR_EGAL expression_valeur ) ;

zone_memoire : ZONE? VARIABLE ;
ZONE : 'la zone mémoire' ;

selection : 
    'Si' si=condition ALORS RETCHAR instruction+ 
        sinon_si* 
        sinon? 
     TAB+ POINT_INTERROGATION RETCHAR ;
 
ALORS : 'alors' ;
sinon_si : TAB+ 'sinon si' condition ALORS RETCHAR instruction+ ;
sinon : TAB+ 'et sinon' RETCHAR instruction+ ;

condition : ((left=expression_valeur operateur_comparaison right=expression_valeur) | (VARIABLE EST VRAI|FAUX)) postcondition* ;
postcondition: operateur_booleen condition ;

operateur_comparaison : (OPERATEUR_COMPARAISON_EGAL | OPERATEUR_DIFFERENT) ;
OPERATEUR_COMPARAISON_EGAL : 'vaut' | 'est égal à' | '==' ;
OPERATEUR_DIFFERENT : 'est différent de' | 'n\'est pas égal à' | '!=' | '<>' ;
VRAI: 'vrai' ;
FAUX: 'faux' ;

operateur_booleen : (ET | OU | OU_EXCLUSIF) ;
ET: 'et';
OU: 'ou';
OU_EXCLUSIF: 'ou au contraire';
EST: 'est' ;

OPERATEUR_EGAL : '=' ;
VARIABLE : '#' (MOT|VALEUR_NOMBRE) ;

expression_valeur : (expression_textuelle | expression_numeraire | expression_variable) ;

expression_variable : ('la valeur de')? VARIABLE;

expression_textuelle : LE_TEXTE? VALEUR_TEXTE ;
LE_TEXTE : 'le texte' ;
VALEUR_TEXTE : '"' ~["]* '"' ;

expression_numeraire : LE_NOMBRE? VALEUR_NOMBRE ;
LE_NOMBRE : 'le nombre' ;
VALEUR_NOMBRE : CHIFFRE+ ;

//fragment LIGNE_DE_TEXTE : MOT (' ' MOT)* ;

MOT : LETTRE+ ;

fragment CHIFFRE : '0'..'9' ;

fragment MINUSCULE : 'a'..'z' ;
fragment MAJUSCULE : 'A'..'Z' ;
fragment SYMBOLES_LETTRE : [-_] ;
fragment LETTRE : MINUSCULE | MAJUSCULE | SYMBOLES_LETTRE ;

VIRGULE : ',' ;
POINT: '.' ;
POINT_INTERROGATION: '?' ;
SUIVANT : '>>' ;
DEUX_POINT: ':' ;

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
