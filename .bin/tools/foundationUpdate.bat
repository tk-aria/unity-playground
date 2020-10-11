git remote add template https://github.com/tk-aria/unity-boilerplate.git
git fetch template
git merge --allow-unrelated-histories template/master
git remote rm template
