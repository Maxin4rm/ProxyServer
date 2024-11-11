# ProxyApp
web_test содержит клиент с измененными url'ами на прокси.
proxyClient содержит графический интерфейс для взаимодействия с API, на reactjs(запускается по командам npm install -> npm start).
Остальные файлы содержат данные по серверной части: запускается с использованием команды docker-compose up в корневой папке 
(Для импорта бд из файла дампа(dump.sql в корневой папке репозитория) следует выполнить команду Get-Content dump.sql | docker exec -i postgres psql -U postgres -d ProxyServerData в терминале из корневой папки проекта)
