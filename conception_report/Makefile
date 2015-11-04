.PHONY: rapport

rapport:
	@pdflatex rapport.tex && pdflatex rapport.tex && printf "\033[42m\n\t\033[1;37mRAPPORT.PDF : SUCCESS\033[21m\n\033[0m\n" || printf "\033[41m\n\t\033[1;37mRAPPORT.PDF : ERROR\033[21m\n\033[0m\n"

clean:
	@rm -f *.log *.aux *.toc *.pdf && printf "\033[42m\n\t\033[1;37mCLEAN : SUCCESS\033[21m\n\033[0m\n" || printf "\033[41m\n\t\033[1;37mCLEAN : ERROR\033[21m\n\033[0m\n"

show:
	@evince rapport.pdf&
