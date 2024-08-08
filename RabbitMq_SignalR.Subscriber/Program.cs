using RabbitMq_SignalR.Subscriber.SubscriberProjects;

var rabbitMqBasicTutorial = new RabbitMqBasicTutorial();
// rabbitMqBasicTutorial.SubscriberMessages();

var rabbitMqFanoutExchangeTutorial = new RabbitMqFanoutExchangeTutorial();
//rabbitMqFanoutExchangeTutorial.SubscriberMessages();

var rabbitMqDirectExchangeTutorial = new RabbitMqDirectExchangeTutorial();
rabbitMqDirectExchangeTutorial.SubscribeMessages();