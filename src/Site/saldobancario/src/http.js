import axios from 'axios'
import router from './router'

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

axios.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded'
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*'
axios.defaults.headers.common['Accept'] = 'application/json, text/plain, */*'
axios.defaults.headers.common['Access-Control-Allow-Headers'] = 'Origin, Accept, Content-Type, Authorization, Access-Control-Allow-Origin'

http.interceptors.response.use(response => {
  return response
}, function (error) {
  let httpStatus = error.response.status
  switch (httpStatus) {
    case 401: {
      break
    }

    case 500: {
      break
    }
  }

  return Promise.reject(error)
})

export default http
