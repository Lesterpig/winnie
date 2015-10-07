/*!
 * \file  sequences.h
 * \brief  Sequences reading 
 * Read new nucleotide or protein sequences
*/

#ifndef SEQ_H
#define SEQ_H

#include <set>
#include <string>
#include <iostream>

/*! \brief Alphabet class. Used to validate a character against an alphabet. */
class alpha 
{
    
    public:
	
	/*! \brief Constructor. Builds an alphabet from an input string.
	* \param s String used as alphabet */
	alpha(const std::string & s) 
	{
	    for(std::string::const_iterator c = s.begin();c != s.end(); c++ ) 
	    _cs.insert(*c);
	}
	
	/*! \brief Checks if the given character is in the alphabet.
	* \param c Character to validate against the alphabet.
	* \return True if the character is in the alphabet. False otherwise. */
	bool is_in_alpha(char c) const { return (_cs.find(c)!=_cs.end()); }

    private:
	
	/*! The alphabet */
	std::set<char> _cs;
	
};


/*! \brief Generic sequence with alphabet. */
class seqmac 
{
    public:
	
	/*! \brief Constructor. Builds the sequence from an input string and an alphabet.
	* \param seq Sequence of characters
	* \param name Name of the sequence
	* \param alphabet alphabet to be used to verify the input sequence */
	seqmac (const std::string & seq, const std::string & name, const std::string & alphabet);
	
	/*! \brief Output the sequence to an output stream.
	* \param os Output stream
	* \param seq Sequence
	* \return The output stream */
	friend std::ostream & operator<< (std::ostream & os, const seqmac & seq);

	/*! \brief An exception used when encountering wrong character in the sequence */
	class UnknownCharError : public std::exception {
		private:
			char _c;
		public:
			UnknownCharError(char c) : _c(c) {}
			char getChar() { return _c; }
	};
	
    protected:
	
	/*! Sequence */
	std::string _seq;
	
	/*! Name */
	std::string _name;

    private:
	
	/*! Alphabet */
	const alpha _alph;
		
	/*! \brief Formatted output of the sequence.
	* \param os Output stream */
	void writeseq(std::ostream & os) const 
	{
	    os << "SEQUENCE" << std::endl << "--------------\n";
	    os << "Nom : " << _name << std::endl;
	    os << "Seq : " << _seq << std::endl;
	    os << "aa  : " << _seq.size() << std::endl;
	}
};


/*! \brief Specialized sequence for proteins. */
class seqprot: public seqmac
{
    
    public:
	
	/*! \brief Constructor. Builds the proteine sequence from an input string with alphabet check. The alphabet is hardcoded for protein characters.
	* \param seq Sequence of characters
	* \param name Name of the sequence */
	seqprot(const std::string & seq="", const std::string & name="") : seqmac(seq,name,"ACDEFGHIKLMNPQRSTV") {}
	
};
     
     
/*! \brief Specialized sequence for ADN. */
class seqadn: public seqmac 
{
    
    public:
	
	/*! \brief Constructor. Builds the ADN sequence from an input string with alphabet check. The alphabet is hardcoded for ADN characters.
	* \param seq Sequence of characters
	* \param name Name of the sequence */
	seqadn(const std::string & seq="", const std::string & name="");
  
};


/*! \brief Specialized sequence for ARN. */
class seqarn: public seqmac 
{

    public:
	
	/*! \brief Constructor. Builds the ARN sequence from an input string with alphabet check. The alphabet is hardcoded for ARN characters.
	* \param seq Sequence of characters
	* \param name Name of the sequence */
	seqarn(const std::string & seq="", const std::string & name="");
	
};

#endif // SEQ_H