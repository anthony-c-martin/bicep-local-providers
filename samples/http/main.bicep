provider http

param coords {
  lattitude: string
  longitude: string
}

resource gridpointsReq 'HttpRequest' = {
  uri: 'https://api.weather.gov/points/${coords.lattitude},${coords.longitude}'
  format: 'raw'
}

var gridpoints = json(gridpointsReq.body).properties

resource forecastReq 'HttpRequest' = {
  uri: 'https://api.weather.gov/gridpoints/${gridpoints.gridId}/${gridpoints.gridX},${gridpoints.gridY}/forecast'
  format: 'raw'
}

var forecast = json(forecastReq.body).properties

type forecastType = {
  name: string
  temperature: int
}

output forecast forecastType[] = map(forecast.periods, p => {
  name: p.name
  temperature: p.temperature
})
