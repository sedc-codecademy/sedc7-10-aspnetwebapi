# Homework due on 19.08.2019

## Regular

1. Make a WebAPI server that will transform strings. The format of the endpoint should be:

`https://{server-url}/api/strings/{input-text}`;

The input text can contain spaces.  

The server should recieve this text and reply with a string transformations response. I.e. if the text has the value `Wekoslav Stefanovski`, the response should be:

```
{
	"lowerCase": "wekoslav stefanovski",
	"upperCase": "WEKOSLAV STEFANOVSKI",
	"titleCase": "Wekoslav Stefanovski" // every first letter capitalized
	"sentenceCase": "Wekoslav stefanovski" // only the first letter of the first word is capitalized,
	"camelCase": {
		"lower": "wekoslavStefanovski", // first letter is lowercase
		"upper": "WekoslavStefanovski" // first letter is uppercase
	},
	"kebabCase": "wekoslav-stefanovski",
	"underscoreCase": "WEKOSLAV_STEFANOVSKI"
}
```

2. Make a client that will use the api to transform text entered by the user.

## Bonus

In addition to this endpoint, create another endpoint with the following format: 

`https://{server-url}/api/strings/{type}/{input-text}`

where `type` will be one of the following values:

```
lowerCase
upperCase
titleCase
sentenceCase
upperCamelCase
lowerCamelCase
kebabCase
underscoreCase
```

The response for `type` of `lowerCase` and `input-text` of `Wekoslav Stefanovski` should be:

```
{
    "result": "wekoslav stefanovski"
}
```

## Bonus Bonus

Add another endpoint (in another controller if needed) in the format  

`https://{server-url}/api/numbers/{number}`

where the response will return the same number in textual format in english, i.e. if the value of number is `12345`, the result should be:

```
{
    "result": {
        "number": 12345,
        "text": "Eleven thousand three hundred and forty five"
    }
}
```

