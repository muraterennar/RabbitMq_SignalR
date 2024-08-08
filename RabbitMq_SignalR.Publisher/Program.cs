using RabbitMq_SignalR.Publisher.PublisherProjects;

// RabbitMq Starter Tutorial
var rabbitMqBasicTutorial = new RabbitMqBasicTutorial();
//rabbitMqBasicTutorial.PublishMessages();

// RabbitMq Fanout Exchange Tutorial
var rabbitMqFanoutExchangeTutorial = new RabbitMqFanoutExchangeTutorial();
//rabbitMqFanoutExchangeTutorial.PublishMessages();

// RabbitMq Direct Exchange Tutorial
var rabbitMqDirectExchangeTutorial = new RabbitMqDirectFanoutExchange();
rabbitMqDirectExchangeTutorial.PublishMessages();