#  Makefile  [Author] tk-aria. 
.DEFAULT_GOAL := help 
MAKEFILE_DIR := $(dir $(lastword $(MAKEFILE_LIST)))
USER_NAME := 'tkaria'

.PHONY: help
help:
	@echo "\n>> help [ command list ]"
	@grep -E '^.PHONY: [a-zA-Z_-]+.*?##' $(MAKEFILE_LIST) | awk 'BEGIN {FS = " "}; {printf "\033[35m%-30s\033[32m %s\n", $$2, $$4}'
	@echo ""

.PHONY: setup ## [setup]リポジトリの設定を行います.
setup:
	git submodule update --init --recursive
	npm install

.PHONY: init ## [category]`description`.
init:
	@echo 'hello world'
	
	git config --local user.name tkaria 
	git config --local user.email tkaria.info@gmail.com
	git commit --amend 
	git submodule update --init 
	git checkout -b release
	git checkout -b develop
	git checkout -b feature/work
	chmod 744 ./**/*.sh
	chmod 744 ./**/*.command

	git clone add https://github.com/Unity-Technologies/UnityCsReference.git ../../Documents/UnityCsReference
	git submodule add https://github.com/TK-Aria/Unity.ScriptTemplates-Plugin.git Assets/ScriptTemplates
	#"com.realgames.dear-imgui": "https://github.com/realgamessoftware/dear-imgui-unity.git",
	
.PHONY: install ## [install]サブモジュールの初期設定.
install: 
	git submodule add https://github.com/Unity-Technologies/UnityCsReference.git ../../Documents/UnityCsReference
	git submodule add https://github.com/TK-Aria/Unity.ScriptTemplates-Plugin.git Assets/ScriptTemplates
	git submodule add https://github.com/TK-Aria/UnityForGit.git Assets/Plugins/UnityForGit

	git submodule update --init --recursive
	git commit -m "[new] add submodule"

.PHONY: foundation_update ## [category]`基盤の更新を行います`.
foundation_update:
	cd "$(PWD)/../../Binary/scripts/" && ./foundationUpdate.sh

.PHONY: release ## [macro]releaseへ移動してワークスペースをマージします.
release: 
	git fetch
	git checkout release
	git merge origin/release
	git branch
	git log --oneline --graph

.PHONY: develop ## [macro]developへ移動してワークスペースをマージします.
develop: 
	git fetch
	git checkout develop
	git merge origin/develop
	git branch
	git log --oneline --graph

.PHONY: feature ## [macro]featureへ移動してワークスペースをマージします.
feature:
	git fetch
	git checkout feature/work
	git merge origin/feature/work
	git branch
	git log --oneline --graph

.PHONY: adjust ## [Git]リモートリポジトリを同一内容に更新します.
adjust:
	git checkout develop
	git merge feature/work
	git push --set-upstream origin develop
	git checkout master
	git merge develop
	git push --set-upstream origin master
	git checkout release
	git merge master
	git push --set-upstream origin release

.PHONY: unitycsreference ## [unity]スクリプトリファレンスを開きます.
unitycsreference:
	vscode ../../Documents/UnityCsReference
