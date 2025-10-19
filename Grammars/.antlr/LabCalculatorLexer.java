// Generated from d:/Documents/uni/2 курс/OOP/lab1/ExcelMAUIApp/ExcelMAUIApp/Grammars/LabCalculator.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue", "this-escape"})
public class LabCalculatorLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		NUMBER=1, IDENTIFIER=2, INT=3, EXPONENT=4, MULTIPLY=5, DIVIDE=6, SUBTRACT=7, 
		ADD=8, EQ=9, NEQ=10, LTE=11, GTE=12, LT=13, GT=14, LPAREN=15, RPAREN=16, 
		WS=17;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"NUMBER", "IDENTIFIER", "INT", "EXPONENT", "MULTIPLY", "DIVIDE", "SUBTRACT", 
			"ADD", "EQ", "NEQ", "LTE", "GTE", "LT", "GT", "LPAREN", "RPAREN", "WS"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, null, null, null, "'^'", "'*'", "'/'", "'-'", "'+'", "'='", "'<>'", 
			"'<='", "'>='", "'<'", "'>'", "'('", "')'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "NUMBER", "IDENTIFIER", "INT", "EXPONENT", "MULTIPLY", "DIVIDE", 
			"SUBTRACT", "ADD", "EQ", "NEQ", "LTE", "GTE", "LT", "GT", "LPAREN", "RPAREN", 
			"WS"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public LabCalculatorLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "LabCalculator.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\u0004\u0000\u0011Z\u0006\uffff\uffff\u0002\u0000\u0007\u0000\u0002\u0001"+
		"\u0007\u0001\u0002\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004"+
		"\u0007\u0004\u0002\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007"+
		"\u0007\u0007\u0002\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b"+
		"\u0007\u000b\u0002\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002"+
		"\u000f\u0007\u000f\u0002\u0010\u0007\u0010\u0001\u0000\u0001\u0000\u0001"+
		"\u0000\u0003\u0000\'\b\u0000\u0001\u0001\u0004\u0001*\b\u0001\u000b\u0001"+
		"\f\u0001+\u0001\u0001\u0001\u0001\u0005\u00010\b\u0001\n\u0001\f\u0001"+
		"3\t\u0001\u0001\u0002\u0004\u00026\b\u0002\u000b\u0002\f\u00027\u0001"+
		"\u0003\u0001\u0003\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005\u0001"+
		"\u0006\u0001\u0006\u0001\u0007\u0001\u0007\u0001\b\u0001\b\u0001\t\u0001"+
		"\t\u0001\t\u0001\n\u0001\n\u0001\n\u0001\u000b\u0001\u000b\u0001\u000b"+
		"\u0001\f\u0001\f\u0001\r\u0001\r\u0001\u000e\u0001\u000e\u0001\u000f\u0001"+
		"\u000f\u0001\u0010\u0001\u0010\u0001\u0010\u0001\u0010\u0000\u0000\u0011"+
		"\u0001\u0001\u0003\u0002\u0005\u0003\u0007\u0004\t\u0005\u000b\u0006\r"+
		"\u0007\u000f\b\u0011\t\u0013\n\u0015\u000b\u0017\f\u0019\r\u001b\u000e"+
		"\u001d\u000f\u001f\u0010!\u0011\u0001\u0000\u0004\u0002\u0000AZaz\u0001"+
		"\u000019\u0001\u000009\u0003\u0000\t\n\r\r  ]\u0000\u0001\u0001\u0000"+
		"\u0000\u0000\u0000\u0003\u0001\u0000\u0000\u0000\u0000\u0005\u0001\u0000"+
		"\u0000\u0000\u0000\u0007\u0001\u0000\u0000\u0000\u0000\t\u0001\u0000\u0000"+
		"\u0000\u0000\u000b\u0001\u0000\u0000\u0000\u0000\r\u0001\u0000\u0000\u0000"+
		"\u0000\u000f\u0001\u0000\u0000\u0000\u0000\u0011\u0001\u0000\u0000\u0000"+
		"\u0000\u0013\u0001\u0000\u0000\u0000\u0000\u0015\u0001\u0000\u0000\u0000"+
		"\u0000\u0017\u0001\u0000\u0000\u0000\u0000\u0019\u0001\u0000\u0000\u0000"+
		"\u0000\u001b\u0001\u0000\u0000\u0000\u0000\u001d\u0001\u0000\u0000\u0000"+
		"\u0000\u001f\u0001\u0000\u0000\u0000\u0000!\u0001\u0000\u0000\u0000\u0001"+
		"#\u0001\u0000\u0000\u0000\u0003)\u0001\u0000\u0000\u0000\u00055\u0001"+
		"\u0000\u0000\u0000\u00079\u0001\u0000\u0000\u0000\t;\u0001\u0000\u0000"+
		"\u0000\u000b=\u0001\u0000\u0000\u0000\r?\u0001\u0000\u0000\u0000\u000f"+
		"A\u0001\u0000\u0000\u0000\u0011C\u0001\u0000\u0000\u0000\u0013E\u0001"+
		"\u0000\u0000\u0000\u0015H\u0001\u0000\u0000\u0000\u0017K\u0001\u0000\u0000"+
		"\u0000\u0019N\u0001\u0000\u0000\u0000\u001bP\u0001\u0000\u0000\u0000\u001d"+
		"R\u0001\u0000\u0000\u0000\u001fT\u0001\u0000\u0000\u0000!V\u0001\u0000"+
		"\u0000\u0000#&\u0003\u0005\u0002\u0000$%\u0005.\u0000\u0000%\'\u0003\u0005"+
		"\u0002\u0000&$\u0001\u0000\u0000\u0000&\'\u0001\u0000\u0000\u0000\'\u0002"+
		"\u0001\u0000\u0000\u0000(*\u0007\u0000\u0000\u0000)(\u0001\u0000\u0000"+
		"\u0000*+\u0001\u0000\u0000\u0000+)\u0001\u0000\u0000\u0000+,\u0001\u0000"+
		"\u0000\u0000,-\u0001\u0000\u0000\u0000-1\u0007\u0001\u0000\u0000.0\u0007"+
		"\u0002\u0000\u0000/.\u0001\u0000\u0000\u000003\u0001\u0000\u0000\u0000"+
		"1/\u0001\u0000\u0000\u000012\u0001\u0000\u0000\u00002\u0004\u0001\u0000"+
		"\u0000\u000031\u0001\u0000\u0000\u000046\u000209\u000054\u0001\u0000\u0000"+
		"\u000067\u0001\u0000\u0000\u000075\u0001\u0000\u0000\u000078\u0001\u0000"+
		"\u0000\u00008\u0006\u0001\u0000\u0000\u00009:\u0005^\u0000\u0000:\b\u0001"+
		"\u0000\u0000\u0000;<\u0005*\u0000\u0000<\n\u0001\u0000\u0000\u0000=>\u0005"+
		"/\u0000\u0000>\f\u0001\u0000\u0000\u0000?@\u0005-\u0000\u0000@\u000e\u0001"+
		"\u0000\u0000\u0000AB\u0005+\u0000\u0000B\u0010\u0001\u0000\u0000\u0000"+
		"CD\u0005=\u0000\u0000D\u0012\u0001\u0000\u0000\u0000EF\u0005<\u0000\u0000"+
		"FG\u0005>\u0000\u0000G\u0014\u0001\u0000\u0000\u0000HI\u0005<\u0000\u0000"+
		"IJ\u0005=\u0000\u0000J\u0016\u0001\u0000\u0000\u0000KL\u0005>\u0000\u0000"+
		"LM\u0005=\u0000\u0000M\u0018\u0001\u0000\u0000\u0000NO\u0005<\u0000\u0000"+
		"O\u001a\u0001\u0000\u0000\u0000PQ\u0005>\u0000\u0000Q\u001c\u0001\u0000"+
		"\u0000\u0000RS\u0005(\u0000\u0000S\u001e\u0001\u0000\u0000\u0000TU\u0005"+
		")\u0000\u0000U \u0001\u0000\u0000\u0000VW\u0007\u0003\u0000\u0000WX\u0001"+
		"\u0000\u0000\u0000XY\u0006\u0010\u0000\u0000Y\"\u0001\u0000\u0000\u0000"+
		"\u0005\u0000&+17\u0001\u0000\u0001\u0000";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}