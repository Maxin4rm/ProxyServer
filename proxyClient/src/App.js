import React, { useState, useEffect } from 'react';
import axios from 'axios';

const App = () => {
  const [key, setKey] = useState('');
  const [value, setValue] = useState('');
  const [data, setData] = useState([]);

  const handlePostRequest = async () => {
    try {
      const response = await axios.post(`http://localhost:7176/api/ProxyAccess?key=${key}&value=${value}`);
      console.log('POST response:', response.data);
    } catch (error) {
      console.error('Error during POST request:', error);
    }
  };

  const handleGetRequest = async () => {
    try {
      const response = await axios.get('http://localhost:7176/api/ProxyAccess');
      setData(response.data);
    } catch (error) {
      console.error('Error during GET request:', error);
    }
  };

  return (
    <div>
      Форма для отправки данных
      <div>
        <input 
          type="text" 
          placeholder="Название сервиса" 
          value={key} 
          onChange={(e) => setKey(e.target.value)} 
        />
        <input 
          type="text" 
          placeholder="число запросов" 
          value={value} 
          onChange={(e) => setValue(e.target.value)} 
        />
        <button onClick={handlePostRequest}>Отправить</button>
      </div>
      
      Таблица данных
      <button onClick={handleGetRequest}>Получить данные</button>
      <table>
        <thead>
          <tr>
            <th>Имя сервиса</th>
            <th>Имя пользователя</th>
            <th>Количество запросов пользователя</th>
            <th>Максимальное количество запросов к сервису</th>
          </tr>
        </thead>
        <tbody>
          {data.map((item, index) => (
            <tr key={index}>
              <td>{item.serviceName}</td>
              <td>{item.clientName}</td>
              <td>{item.clientAccessCount}</td>
              <td>{item.clientCurrentAccessCount}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default App;
