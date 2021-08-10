# SocketClient_Server
# 1.Socket
通訊端是支援TCP/IP協定的網路通訊的基本操作單元。可以將通訊端看作不同主機間的程序進行雙向通訊的端點，它構成了單個主機內及整個網路間的程式設計介面。
通訊端的工作原理：
通過網際網路進行通訊，至少需要一對通訊端，其中一個執行於客戶機端，稱之為ClientSocket，另一個執行於伺服器端，稱之為ServerSocket。
通訊端之間的連線過程可以分為三個步驟：伺服器監聽，使用者端請求，連線確認。

# 2.TCP
TCP協定提供的是端到端服務。TCP協定所提供的端到端的服務是保證資訊一定能夠到達目的地址。它是一種面向連線的協定。
TCP程式設計的伺服器端一般步驟

①建立一個socket，用函數socket()


②繫結IP地址、埠等資訊到socket上，用函數bind()

③開啟監聽，用函數listen()

④接收使用者端上來的連線，用函數accept()

⑤收發資料，用函數send()和recv()，或者read()和write()

⑥關閉網路連線；

⑦關閉監聽；

TCP程式設計的使用者端一般步驟

①建立一個socket，用函數socket()

②設定要連線的對方的IP地址和埠等屬性

③連線伺服器，用函數connect(）

④收發資料，用函數send()和recv()，或者read()和write()

⑤關閉網路連線
![2020103016053164550gtgoqajyx](https://user-images.githubusercontent.com/50354880/128818977-eca88a9e-e2e7-4d41-b6da-404448fb73a8.png)
