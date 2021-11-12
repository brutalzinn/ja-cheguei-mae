import random
import os
from enum import Enum, auto
import redis
import json
from telethon.sessions import StringSession
from time import sleep
import requests
import asyncio
import urllib

from telethon import TelegramClient, events
api_id = os.getenv("API_ID")
api_hash = os.getenv("API_HASH")
token = os.getenv("TOKEN")
phone =  os.getenv("NUMERO")
r = redis.Redis(host='localhost',password='SUASENHA', port=6379, db=0)

bot = TelegramClient('Bot', api_id, api_hash)

# We use a Python Enum for the state because it's a clean and easy way to do it
class State(Enum):
    WAIT_RUNNING = auto()
    WAIT_EMAIL = auto()
    WAIT_TOKEN = auto()
    WAIT_START = auto()


# The state in which different users are, {user_id: state}
conversation_state = {}
conversation_state['running'] = State.WAIT_RUNNING
@bot.on(events.NewMessage(pattern='(?i)hi|hello|start|login'))
async def starthandle(event):
    conversation_state['running'] = State.WAIT_START
@bot.on(events.NewMessage)
async def handler(event):
    if conversation_state['running'] == State.WAIT_START:
        who = event.sender_id
        state = conversation_state.get(who)
    #  client = TelegramClient(StringSession(), api_id, api_hash)
        if state is None:
            await bot.send_message(event.chat, 'Olá. Bem vindo ao sistema de login da api ja cheguei mae.')
            await bot.send_message(event.chat, 'Esse é um ambiente de testes. Não use sua conta original.')
            await event.respond('Olá! Qual é seu email?')
            conversation_state[who] = State.WAIT_TOKEN
        elif state == State.WAIT_TOKEN:   
            data = {
                'id':event.chat.id,
                'username':event.chat.username,
                'first_name':event.chat.first_name,
                'last_name':event.chat.last_name,
                'email':event.text        
            }
            r.set(event.text, json.dumps(data))
            await event.respond(f'Obrigado, adicionei sua chave no banco de dados.')
            await event.respond(f'Adeus..')
            conversation_state['running'] = State.WAIT_RUNNING
            sleep(5)
            del conversation_state[who]

async def TelegramEnviarMensagem(email: str, mensagem: str):
    data = json.loads(r.get(email))
    #conversation_state['running'] = State.WAIT_RUNNING
    requests.get(f'https://api.telegram.org/bot{token}/sendMessage?chat_id={data["id"]}&text={urllib.parse.quote(mensagem)}')
    
async def parseurls():
    while True:
        ts = abs(int(random.random()*10))
        print(f'parseurls({ts})')
        await sendmsg(ts)
        await asyncio.sleep(ts)


async def sendmsg(msg):
    print(f'sendmsg({msg}) - start')
    channel = await bot.get_entity('https://t.me/bobertobot')
    await bot.send_message(channel, f'ответ из другого потока {msg}')
    print(f'sendmsg({msg}) - done')


