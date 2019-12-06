# Saldo Bancário

## Project setup
```
npm install
```

### UrlBase Configuration
Configurar a url de comunicação com a API através da chave baseURL no arquivo http.js conforme exemplo abaixo:
```
const http = axios.create({
  baseURL: 'https://localhost:44343/api/',
  headers: {
    Authorization: {
      toString () {
        return `Bearer ${localStorage.getItem('token')}`
      }
    }
  }
})
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Run your tests
```
npm run test
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).
