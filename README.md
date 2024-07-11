ImtStateMachineをUniTaskで動かしたいと思って作成

Enterでawaitをかけると終わるまでUpdateに移行しない。
Updateでawaitをかけると終わるまで次のUpdateは呼ばれない。
Exitでawaitをかけると終わるまで次のステートに移動しない。

----------------
# ImtStateMachine

元々別リポジトリ([IceMilkTea](https://github.com/Sinoa/IceMilkTea))のサブセットとして実装されていたステートマシンを、単体パッケージとして配布することを決めましたので、独立したリポジトリとして用意しました。

## ImtStateMachineについて(About IceMilkTea)

### 作者

* Sinoa <sinoans@gmail.com>

### 開発協力者

[Contributors](https://github.com/Sinoa/ImtStateMachine/graphs/contributors)を参照

* 旧IceMilkTeaからのコントリビュータ引き継ぎ
  * [shiena](https://github.com/shiena)
    * IL2CPPビルドに関する実装支援
  * [ksatoudeveloper](https://github.com/ksatoudeveloper)
    * ImtStateMachineのイベントをジェネリック化検討及び実装支援

### ライセンス

* [Zlib](https://opensource.org/licenses/Zlib)
