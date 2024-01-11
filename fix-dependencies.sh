/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
brew install freeimage
sudo mkdir /usr/local/lib
sudo ln -s /opt/homebrew/Cellar/freeimage/3.18.0/lib/libfreeimage.dylib /usr/local/lib/libfreeimage
brew install freetype
sudo ln -s /opt/homebrew/lib/libfreetype.6.dylib /usr/local/lib/libfreetype6