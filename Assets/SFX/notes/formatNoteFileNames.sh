#!/bin/bash
# -- Render instructions --
# 1. Select all tracks with audio to be rendered (ie: not the root instrument track).
#	 To make this script's job easier, do not add dashes to the file name.
# 2. Render settings: 
#    Source: Selected tracks via master
#    Bounds: Time selection

# Ensure the right command line args are supplied
if [ $# != 3 ]; then
	echo "Usage: $0 [Target Dir] [Original file grep term] [Octave]"
	echo "Example: $0 ./ pulse_25 3"
	echo "	(Outputs pulse_25_C3.ogg)"
	exit
fi

count=0
# Iterate through the supplied files in this dir
for i in `ls "$1" | grep "$2"`; do
	filename=`echo "$i" | cut -d '-' -f 1`
	noteID=`echo "$i" | cut -d '-' -f 2 | cut -d '.' -f 1`
	fileExtension=`echo "$i" | cut -d '.' -f 2`

	#echo "  $filename"
	#echo "  $noteID"
	#echo "  $fileExtention"

	case $noteID in
		'001')
			noteName="C"
			;;
		"002")
			noteName="Cs"
			;;
		"003")
			noteName="D"
			;;
		"004")
			noteName="Ds"
			;;
		"005")
			noteName="E"
			;;
		"006")
			noteName="F"
			;;
		"007")
			noteName="Fs"
			;;
		"008")
			noteName="G"
			;;
		"009")
			noteName="Gs"
			;;
		"010")
			noteName="A"
			;;
		"011")
			noteName="As"
			;;
		"012")
			noteName="B"
			;;
		*)
			noteName="UNKNOWN"
			;;
	esac

	# filename: pulse_25
	# octave: 3
	# note index: 0
	# note name: C
	# extension: .ogg
	#	Final: pulse_25_3_0_C3.ogg
	newFileName="${1}/${filename}_${3}_${count}_${noteName}.${fileExtension}"

	echo "${1}/${i} -> ${newFileName}"
	mv "${1}/${i}" "${newFileName}"

	((count=count+1))
done
