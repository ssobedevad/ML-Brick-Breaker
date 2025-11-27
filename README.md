A simple ML project to test the ml agents python and unity compatibility.

The agent has adapted to perform a simple brick breaker situation.

This was about 10 hours of training and the result is decent.

The rules for score were as follows:

Reward breaking brick (double if not hit paddle since last brick to reward combo)

Reward hitting ball after hitting brick

Reward clearing all bricks

Penalise giving a movement command (to reduce shakiness)

Overall the agent went from scoring an average of ~0.05 bricks per attempt to ~40 after training out of a total of 64.

I used random start position and velocity on the ball to promote adaptability

The ball speeds up after not being hit by a small amount and resets when hit by the paddle
