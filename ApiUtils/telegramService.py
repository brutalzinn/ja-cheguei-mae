# import os
# from telethon import TelegramClient
# from telethon.sessions import StringSession
# import asyncio
# import redis
# import random
# api_id = os.getenv("API_ID")
# api_hash = os.getenv("API_HASH")
# token = os.getenv("TOKEN")
# phone =  os.getenv("NUMERO")
# r = redis.Redis(host='localhost',password='SUASENHA', port=6379, db=0)
# bot = TelegramClient('Bot', api_id, api_hash)
# async def TelegramEnviarMensagem(email: str, mensagem: str):
    
    
#     client = TelegramClient(StringSession(redis.get(email)), api_id, api_hash)
#     await client.connect()
#     if not await client.is_user_authorized():
#         await client.send_code_request(phone)
#         await client.sign_in(phone, input('Enter the code: ')) 
#     try:
#         await client.send_message(numero, mensagem, parse_mode='html')
#     except Exception as e:
#         print(e)
#     await client.disconnect()
# list = ['Daniel detesta python', 'Daniel fala muito de linux', 'Torta de melão é horrível',
#         'Uber é intermediário do capitalismo', 'Todos detestamos o bitbucket']
# asyncio.run(TelegramEnviarMensagem('contato@robertocpaes.dev',phone,random.choice(list)))