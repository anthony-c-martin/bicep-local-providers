provider http

resource forecast 'HttpRequest' = {
  uri: 'https://api.weather.gov/gridpoints/SEW/131,69/forecast'
  format: 'raw'
}

var properties = json(forecast.body).properties

type forecastType = {
  name: string
  temperature: int
}

output forecast forecastType[] = map(properties.periods, p => {
  name: p.name
  temperature: p.temperature
})
