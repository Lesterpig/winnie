#ifndef FRACTION_H
#define FRACTION_H

#include <exception>

class Fraction {

private:
	int _n;
	int _d;

	static int safeAdd(int a, int b);
	static int safeSub(int a, int b);
	static int safeMul(int a, int b);
	static int safeDiv(int a, int b);

	// @TODO add simplify()

public:
	Fraction(int n);
	Fraction(int n, int d);
	
	friend Fraction operator+(const Fraction& a, const Fraction& b);
	friend Fraction operator-(const Fraction& a, const Fraction& b);
	friend Fraction operator*(const Fraction& a, const Fraction& b);
	friend Fraction operator/(const Fraction& a, const Fraction& b);

	double eval();

	// Exceptions

	class Error : public std::exception {
	public:
		virtual const char* what(void);
	};

	class DivideByZeroError : public Fraction::Error {
	public:
		virtual const char* what(void);
	};

	class OverflowError : public Fraction::Error {
	public:
		virtual const char* what(void);
	};

};

Fraction operator+(const Fraction& a, const Fraction& b);
Fraction operator-(const Fraction& a, const Fraction& b);
Fraction operator*(const Fraction& a, const Fraction& b);
Fraction operator/(const Fraction& a, const Fraction& b);

#endif