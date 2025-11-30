for i in $(seq "$1"); do
  if [ "$i" -eq 1 ]; then
      echo
  fi
  echo " Run $i"
  ./run.sh "${@:2}" true
done
