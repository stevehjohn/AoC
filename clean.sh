find . -name "*.orig" -type f -delete
find . -type d \( -name bin -o -name obj \) -exec rm -rf {} +

