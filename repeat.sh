for i in $(seq "$1"); do echo "\n Run $i"; ./run.sh "${@:2}" true; done
