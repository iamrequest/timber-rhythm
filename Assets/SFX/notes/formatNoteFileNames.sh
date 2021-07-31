#!/bin/bash
# -- Render instructions --
# 1. Select all tracks with audio to be rendered (ie: not the root instrument track).
#	 To make this script's job easier, do not add dashes to the file name.
# 2. Render settings: 
#    Source: Selected tracks via master
#    Bounds: Time selection

# Ensure the right command line args are supplied
if [ $# != 3 ]; then
	echo "Usage: $0 [Target Dir] [Original file grep term] [New file postfix]"
	echo "Example: $0 ./ pulse_25 3"
	echo "	(Outputs pulse_25_C3.ogg)"
	exit
fi

# Iterate through the supplied files in this dir
for i in `ls "$1" | grep "$2"`; do
	filename=`echo "$i" | cut -d '-' -f 1`
	noteID=`echo "$i" | cut -d '-' -f 2 | cut -d '.' -f 1`
	fileExtension=`echo "$i" | cut -d '.' -f 2`

	case $noteID in
		'000')
			noteName="C"
			;;
		"001")
			noteName="Cs"
			;;
		"002")
			noteName="D"
			;;
		"003")
			noteName="Ds"
			;;
		"004")
			noteName="E"
			;;
		"005")
			noteName="F"
			;;
		"006")
			noteName="Fs"
			;;
		"007")
			noteName="G"
			;;
		"008")
			noteName="Gs"
			;;
		"009")
			noteName="A"
			;;
		"010")
			noteName="As"
			;;
		"011")
			noteName="B"
			;;
		"012")
			noteName="Bs"
			;;
		*)
			noteName="UNKNOWN"
			;;
	esac

	# Base filename, note name, new postfix, file extension
	# Eg:
	#	pulse_25
	#	C
	#	3
	#	.ogg
	# Final: pulse_25_C3.ogg
	newFileName="${1}/${filename}_${noteName}${3}.${fileExtension}"
	#echo "${filename}_${noteName}${3}.${fileExtension}"

	echo "${1}/${i} -> ${newFileName}"
	mv "${1}/${i}" "${newFileName}"
done
