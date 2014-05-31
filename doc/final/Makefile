report.pdf: report.tex report.bib
	pdflatex report
	biber report
	pdflatex report
	pdflatex report

clean:
	rm -f *.aux *.bbl *.bcf *.blg *.log *.out *.pdf *.run.xml *.toc
