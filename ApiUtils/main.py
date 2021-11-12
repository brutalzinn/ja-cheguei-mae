from flask import Flask, request,Response
import asyncio
import os
from boberto import TelegramEnviarMensagem, bot
from dotenv import load_dotenv
import threading

token = os.getenv("TOKEN")

load_dotenv()
app = Flask(__name__)




@app.route('/EnviarMensagem', methods=['POST'])
def EnviarMensagem():
    data = request.json  
    asyncio.run(TelegramEnviarMensagem(data['email'], data['mensagem']))
    return Response(status=200)

if __name__ == "__main__":
    threading.Thread(target=lambda: app.run(debug=True, use_reloader=False)).start()
    bot.start(bot_token=token)

    loop = asyncio.get_event_loop()
    tasks = [
        loop.create_task(bot.run_until_disconnected()),
       
    ]
 
    loop.run_until_complete(asyncio.wait(tasks))
    loop.close()

