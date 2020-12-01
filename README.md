# KeyTouchView
KeyTouchViewは キー操作、マウス操作を可視化するためのアプリケーションです。

たとえば、KeyTouchViewをキーボードを使ったアプリケーション等の配信等にオーバーレイすることで、</br>
ユーザーの操作を可視化することができます。

![icon](https://user-images.githubusercontent.com/75171853/100716706-6a1d1c80-33fc-11eb-9376-73834881448a.png)

## 使い方
![First](https://user-images.githubusercontent.com/75171853/100718108-4bb82080-33fe-11eb-820a-d935c1dd387d.png)

フォームを右クリックして、メニューを呼び出します。</br>
初回起動時は、レイアウトの選択がされていないので、好きなレイアウトを選択してください。</br>
レイアウトによっては、補足するキーを追加しないと表示されないので、レイアウトの設定画面を呼び出して追加を行ってください。

---
### 試しに、DefaultLayoutの設定画面を使って説明します。
###### 下記は、[表示キー]を設定する画面です。

![DefaultLayout](https://user-images.githubusercontent.com/75171853/100718821-1e1fa700-33ff-11eb-9e5c-94eb40e18d65.png)

左側のリストは、補足可能なキー一覧です。</br>
右側のリストは、補足するキー一覧です。</br>
矢印ボタンを使って追加、削除また画面内での順列変更を行います。</br>
また、設定画面がアクティブなときにキー操作を行うと、対応したキーを補足可能なキー一覧から自動でピックアップしてくれます。

---
###### 下記は、[置換キー]を設定する画面です。

![DefaultLayout_Replace](https://user-images.githubusercontent.com/75171853/100720290-1cef7980-3401-11eb-8f9e-fe3ff4eb5756.png)

キーの初期名を別の名前に変更することができます。</br>
こちらも、設定画面がアクティブなときにキー操作を行うと、対応したキーを補足可能なキー一覧から自動でピックアップしてくれます。

---
###### [その他]を設定する画面です。

![DefaultLayout_Ext](https://user-images.githubusercontent.com/75171853/100721357-950a6f00-3402-11eb-8643-2227051b2cf0.png)

こちらは、表示に関する設定を行うことができます。</br>
左側は KeyTouchView 本体で、右の設定を適用し、先程補足キーに追加し、かつキー名を置換した状態の[W]キーを押した画面です。

---
以上でーす
